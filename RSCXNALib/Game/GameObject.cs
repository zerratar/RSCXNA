using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RSCXNALib.Data;
using RSCXNALib.Game.Cameras;
namespace RSCXNALib.Game
{

    public class GameObject //: GameObject
    {

        public GameObject(int vertCount, int polygonCount)
        //: base(_vert_count, polygonCount)
        {
            objectState = 1;
            visible = true;
            chh = true;
            chi = false;
            isGiantCrystal = false;
            index = -1;
            chn = false;
            noCollider = false;
            dontRecieveShadows = false;
            cic = false;
            cid = false;
            shadeValue = 0xbc614e;
            distVar = 0xbc614e;
            cla = 180;
            clb = 155;
            clc = 95;
            cld = 256;
            cle = 512;
            clf = 32;
            InitializeObject(vertCount, polygonCount);
            cje = new int[polygonCount][];
            for (int j = 0; j < polygonCount; j++)
            {
                cje[j] = new int[1];
                cje[j][0] = j;
            }

        }

        public GameObject(int vertCount, int polyCount, bool flag, bool flag1, bool flag2, bool flag3, bool flag4)
        //: base(x, y, flag, flag1, flag2, flag3, flag4)
        {
            objectState = 1;
            visible = true;
            chh = true;
            chi = false;
            isGiantCrystal = false;
            index = -1;
            chn = false;
            noCollider = false;
            dontRecieveShadows = false;
            cic = false;
            cid = false;
            shadeValue = 0xbc614e;
            distVar = 0xbc614e;
            cla = 180;
            clb = 155;
            clc = 95;
            cld = 256;
            cle = 512;
            clf = 32;
            chn = flag;
            noCollider = flag1;
            dontRecieveShadows = flag2;
            cic = flag3;
            cid = flag4;
            InitializeObject(vertCount, polyCount);
        }

        private void InitializeObject(int _vert_count, int polygonCount)
        {
            vert_x = new int[_vert_count];
            vert_y = new int[_vert_count];
            vert_z = new int[_vert_count];

            _vertices = new Vector3[_vert_count];

#warning possibly texture coordinates.
            cfn = new int[_vert_count];
            vertexColor = new int[_vert_count];


            face_vertices_count = new int[polygonCount];
            face_vertices = new int[polygonCount][];
            texture_back = new int[polygonCount];
            texture_front = new int[polygonCount];
            gouraud_shade = new int[polygonCount];
            cgh = new int[polygonCount];
            cgg = new int[polygonCount];
            if (!cid)
            {
                cfi = new int[_vert_count];
                cfj = new int[_vert_count];
                cfk = new int[_vert_count];
                cfl = new int[_vert_count];
                cfm = new int[_vert_count];
            }
            if (!cic)
            {
                chm = new int[polygonCount];
                entityType = new int[polygonCount];
            }
            if (chn)
            {
                worldVertX = vert_x;
                worldVertY = vert_y;
                worldVertZ = vert_z;
            }
            else
            {
                worldVertX = new int[_vert_count];
                worldVertY = new int[_vert_count];
                worldVertZ = new int[_vert_count];
            }
            if (!dontRecieveShadows || !noCollider)
            {
                normalX = new int[polygonCount];
                normalY = new int[polygonCount];
                normalZ = new int[polygonCount];
            }
            if (!noCollider)
            {
                faceBoundsMinX = new int[polygonCount];
                faceBoundsMaxX = new int[polygonCount];
                faceBoundsMinY = new int[polygonCount];
                faceBoundsMaxY = new int[polygonCount];
                faceBoundsMinZ = new int[polygonCount];
                faceBoundsMaxZ = new int[polygonCount];
            }
            face_count = 0;
            vert_count = 0;
            totalVerticeCount = _vert_count;
            totalFaceCount = polygonCount;
            positionX = positionY = positionZ = 0;
            rotationX = rotationY = rotationZ = 0;
            ckd = cke = ckf = 256;
            ckg = ckh = cki = ckj = ckk = ckl = 256;
            ckm = 0;
        }

        public void clj()
        {
            cfi = new int[vert_count];
            cfj = new int[vert_count];
            cfk = new int[vert_count];
            cfl = new int[vert_count];
            cfm = new int[vert_count];
        }

        public void resetObjectIndexes()
        {
            face_count = 0;
            vert_count = 0;
        }

        public void cll(int j, int k)
        {
            face_count -= j;
            if (face_count < 0)
                face_count = 0;
            vert_count -= k;
            if (vert_count < 0)
                vert_count = 0;
        }

        public GameObject(sbyte[] data, int offset, bool arg2)
        //: base(_vert_count, polygonCount, z)
        {
            objectState = 1;
            visible = true;
            chh = true;
            chi = false;
            isGiantCrystal = false;
            index = -1;
            chn = false;
            noCollider = false;
            dontRecieveShadows = false;
            cic = false;
            cid = false;
            shadeValue = 0xbc614e;
            distVar = 0xbc614e;
            cla = 180;
            clb = 155;
            clc = 95;
            cld = 256;
            cle = 512;
            clf = 32;
            int _vert_count = DataOperations.getShort(data, offset);
            offset += 2;
            int _face_count = DataOperations.getShort(data, offset);
            offset += 2;

            InitializeObject(_vert_count, _face_count);
            cje = new int[vert_x.Length][];

            for (int l = 0; l < _vert_count; l++)
            {
                cje[l] = new int[1];                
                vert_x[l] = DataOperations.getShort2(data, offset);
                _vertices[l] = new Vector3(vert_x[l], _vertices[l].Y, _vertices[l].Z);
                offset += 2;
            }

            for (int i1 = 0; i1 < _vert_count; i1++)
            {
                vert_y[i1] = DataOperations.getShort2(data, offset);
                _vertices[i1] = new Vector3(_vertices[i1].X, vert_y[i1], _vertices[i1].Z);
                offset += 2;
            }

            for (int j1 = 0; j1 < _vert_count; j1++)
            {
                vert_z[j1] = DataOperations.getShort2(data, offset);
                _vertices[j1] = new Vector3(_vertices[j1].X, _vertices[j1].Y, vert_z[j1]);
                offset += 2;
            }

            vert_count = _vert_count;
            for (int k1 = 0; k1 < _face_count; k1++)
                face_vertices_count[k1] = data[offset++] & 0xff;

            for (int l1 = 0; l1 < _face_count; l1++)
            {
                texture_back[l1] = DataOperations.getShort2(data, offset);
                offset += 2;
                if (texture_back[l1] == 32767)
                    texture_back[l1] = shadeValue;
            }

            for (int i2 = 0; i2 < _face_count; i2++)
            {
                texture_front[i2] = DataOperations.getShort2(data, offset);
                offset += 2;
                if (texture_front[i2] == 32767)
                    texture_front[i2] = shadeValue;
            }

            for (int j2 = 0; j2 < _face_count; j2++)
            {
                int k2 = data[offset++] & 0xff;
                if (k2 == 0)
                    gouraud_shade[j2] = 0;
                else
                    gouraud_shade[j2] = shadeValue;
            }

            for (int l2 = 0; l2 < _face_count; l2++)
            {
                face_vertices[l2] = new int[face_vertices_count[l2]];
                for (int i3 = 0; i3 < face_vertices_count[l2]; i3++)
                    if (_vert_count < 256)
                    {
                        face_vertices[l2][i3] = data[offset++] & 0xff;
                    }
                    else
                    {
                        face_vertices[l2][i3] = DataOperations.getShort(data, offset);
                        offset += 2;
                    }

            }

            face_count = _face_count;
            objectState = 1;
        }

        public GameObject(String fileName)
        {
            objectState = 1;
            visible = true;
            chh = true;
            chi = false;
            isGiantCrystal = false;
            index = -1;
            chn = false;
            noCollider = false;
            dontRecieveShadows = false;
            cic = false;
            cid = false;
            shadeValue = 0xbc614e;
            distVar = 0xbc614e;
            cla = 180;
            clb = 155;
            clc = 95;
            cld = 256;
            cle = 512;
            clf = 32;
            byte[] abyte0 = null;
            try
            {
                var inputstream = DataOperations.openInputStream(fileName);
                //DataInputStream datainputstream = new DataInputStream(inputstream);
                abyte0 = new byte[3];
                clg = 0;
                for (int j = 0; j < 3; j += inputstream.Read(abyte0, j, 3 - j)) ;
                int l = getShadeValue((sbyte[])(Array)abyte0);
                abyte0 = new byte[l];
                clg = 0;
                for (int k = 0; k < l; k += inputstream.Read(abyte0, k, l - k)) ;
                inputstream.Close();
            }
            catch
            {
                vert_count = 0;
                face_count = 0;
                return;
            }
            int i1 = getShadeValue((sbyte[])(Array)abyte0);
            int j1 = getShadeValue((sbyte[])(Array)abyte0);
            InitializeObject(i1, j1);
            cje = new int[j1][];
            for (int k3 = 0; k3 < i1; k3++)
            {
                int k1 = getShadeValue((sbyte[])(Array)abyte0);
                int l1 = getShadeValue((sbyte[])(Array)abyte0);
                int i2 = getShadeValue((sbyte[])(Array)abyte0);
                getVertexIndex(k1, l1, i2);
            }

            for (int l3 = 0; l3 < j1; l3++)
            {
                int j2 = getShadeValue((sbyte[])(Array)abyte0);
                int k2 = getShadeValue((sbyte[])(Array)abyte0);
                int l2 = getShadeValue((sbyte[])(Array)abyte0);
                int i3 = getShadeValue((sbyte[])(Array)abyte0);
                cle = getShadeValue((sbyte[])(Array)abyte0);
                clf = getShadeValue((sbyte[])(Array)abyte0);
                int j3 = getShadeValue((sbyte[])(Array)abyte0);
                int[] ai = new int[j2];
                for (int i4 = 0; i4 < j2; i4++)
                    ai[i4] = getShadeValue((sbyte[])(Array)abyte0);

                int[] ai1 = new int[i3];
                for (int j4 = 0; j4 < i3; j4++)
                    ai1[j4] = getShadeValue((sbyte[])(Array)abyte0);

                int k4 = addFaceVertices(j2, ai, k2, l2);
                cje[l3] = ai1;
                if (j3 == 0)
                    gouraud_shade[k4] = 0;
                else
                    gouraud_shade[k4] = shadeValue;
            }

            objectState = 1;
        }

        public GameObject(GameObject[] childObjects, int objectCount, bool flag, bool flag1, bool flag2, bool flag3)
        //: base(childObjects, x, flag, flag1, flag2, flag3)
        {
            objectState = 1;
            visible = true;
            chh = true;
            chi = false;
            isGiantCrystal = false;
            index = -1;
            chn = false;
            noCollider = false;
            dontRecieveShadows = false;
            cic = false;
            cid = false;
            shadeValue = 0xbc614e;
            distVar = 0xbc614e;
            cla = 180;
            clb = 155;
            clc = 95;
            cld = 256;
            cle = 512;
            clf = 32;
            chn = flag;
            noCollider = flag1;
            dontRecieveShadows = flag2;
            cic = flag3;
            BuildGameObject(childObjects, objectCount, false);
        }

        public GameObject(GameObject[] childObjects, int objectCount)
        //: base(childObjects, x)
        {
            objectState = 1;
            visible = true;
            chh = true;
            chi = false;
            isGiantCrystal = false;
            index = -1;
            chn = false;
            noCollider = false;
            dontRecieveShadows = false;
            cic = false;
            cid = false;
            shadeValue = 0xbc614e;
            distVar = 0xbc614e;
            cla = 180;
            clb = 155;
            clc = 95;
            cld = 256;
            cle = 512;
            clf = 32;
            BuildGameObject(childObjects, objectCount, true);
        }

        public void BuildGameObject(GameObject[] childObjects, int objectCount, bool arg2)
        {
            int j = 0;
            int k = 0;
            for (int l = 0; l < objectCount; l++)
            {
                j += childObjects[l].face_count;
                k += childObjects[l].vert_count;
            }

            InitializeObject(k, j);
            if (arg2)
                cje = new int[j][];
            for (int i1 = 0; i1 < objectCount; i1++)
            {
                GameObject j1 = childObjects[i1];
                j1.cni();
                clf = j1.clf;
                cle = j1.cle;
                cla = j1.cla;
                clb = j1.clb;
                clc = j1.clc;
                cld = j1.cld;
                for (int k1 = 0; k1 < j1.face_count; k1++)
                {
                    int[] ai = new int[j1.face_vertices_count[k1]];
                    int[] ai1 = j1.face_vertices[k1];
                    for (int l1 = 0; l1 < j1.face_vertices_count[k1]; l1++)
                        ai[l1] = getVertexIndex(j1.vert_x[ai1[l1]], j1.vert_y[ai1[l1]], j1.vert_z[ai1[l1]]);

                    int i2 = addFaceVertices(j1.face_vertices_count[k1], ai, j1.texture_back[k1], j1.texture_front[k1]);
                    gouraud_shade[i2] = j1.gouraud_shade[k1];
                    cgh[i2] = j1.cgh[k1];
                    cgg[i2] = j1.cgg[k1];
                    if (arg2)
                        if (objectCount > 1)
                        {
                            cje[i2] = new int[j1.cje[k1].Length + 1];
                            cje[i2][0] = i1;
                            for (int j2 = 0; j2 < j1.cje[k1].Length; j2++)
                                cje[i2][j2 + 1] = j1.cje[k1][j2];

                        }
                        else
                        {
                            cje[i2] = new int[j1.cje[k1].Length];
                            for (int k2 = 0; k2 < j1.cje[k1].Length; k2++)
                                cje[i2][k2] = j1.cje[k1][k2];

                        }
                }

            }

            objectState = 1;
        }

        public int getVertexIndex(int x, int y, int z)
        {
            for (int j = 0; j < vert_count; j++)
                if (vert_x[j] == x && vert_y[j] == y && vert_z[j] == z)
                    return j;

            if (vert_count >= totalVerticeCount)
            {
                return -1;
            }
            else
            {
                vert_x[vert_count] = x;
                vert_y[vert_count] = y;
                vert_z[vert_count] = z;
                return vert_count++;
            }
        }

        public int addVertex(int x, int y, int z)
        {
            if (vert_count >= totalVerticeCount)
            {
                return -1;
            }
            else
            {
                vert_x[vert_count] = x;
                vert_y[vert_count] = y;
                vert_z[vert_count] = z;
                return vert_count++;
            }
        }

        public int addFaceVertices(int vertexCount, int[] _faceVertices, int _faceBack, int _faceFront)
        {
            if (face_count >= totalFaceCount)
            {
                return -1;
            }
            else
            {
                face_vertices_count[face_count] = vertexCount;
                face_vertices[face_count] = _faceVertices;
                texture_back[face_count] = _faceBack;
                texture_front[face_count] = _faceFront;
                objectState = 1;
                return face_count++;
            }
        }

        public GameObject[] getObjectsWithinArea(int x, int y, int width, int height, int objectSize, int objectCount, int maxVertCount,
                bool arg7)
        {
            cni();
            int[] ai = new int[objectCount];
            int[] ai1 = new int[objectCount];
            for (int j = 0; j < objectCount; j++)
            {
                ai[j] = 0;
                ai1[j] = 0;
            }

            for (int k = 0; k < face_count; k++)
            {
                int l = 0;
                int i1 = 0;
                int k1 = face_vertices_count[k];
                int[] ai3 = face_vertices[k];
                for (int k2 = 0; k2 < k1; k2++)
                {
                    l += vert_x[ai3[k2]];
                    i1 += vert_z[ai3[k2]];
                }

                int i3 = l / (k1 * width) + (i1 / (k1 * height)) * objectSize;
                ai[i3] += k1;
                ai1[i3]++;
            }

            GameObject[] ai2 = new GameObject[objectCount];
            for (int j1 = 0; j1 < objectCount; j1++)
            {
                if (ai[j1] > maxVertCount)
                    ai[j1] = maxVertCount;
                ai2[j1] = new GameObject(ai[j1], ai1[j1], true, true, true, arg7, true);
                ai2[j1].cle = cle;
                ai2[j1].clf = clf;
            }

            for (int l1 = 0; l1 < face_count; l1++)
            {
                int i2 = 0;
                int l2 = 0;
                int j3 = face_vertices_count[l1];
                int[] ai4 = face_vertices[l1];
                for (int k3 = 0; k3 < j3; k3++)
                {
                    i2 += vert_x[ai4[k3]];
                    l2 += vert_z[ai4[k3]];
                }

                int l3 = i2 / (j3 * width) + (l2 / (j3 * height)) * objectSize;
                CopyModelData(ai2[l3], ai4, j3, l1);
            }

            for (int j2 = 0; j2 < objectCount; j2++)
                ai2[j2].clj();

            return ai2;
        }

        public void CopyModelData(GameObject arg0, int[] indices, int indexCount, int entityTypeIndex)
        {
            int[] ai = new int[indexCount];
            for (int j = 0; j < indexCount; j++)
            {
                int k = ai[j] = arg0.getVertexIndex(vert_x[indices[j]], vert_y[indices[j]], vert_z[indices[j]]);
                arg0.cfn[k] = cfn[indices[j]];
                arg0.vertexColor[k] = vertexColor[indices[j]];
            }

            int l = arg0.addFaceVertices(indexCount, ai, texture_back[entityTypeIndex], texture_front[entityTypeIndex]);
            if (!arg0.cic && !cic)
                arg0.entityType[l] = entityType[entityTypeIndex];
            arg0.gouraud_shade[l] = gouraud_shade[entityTypeIndex];
            arg0.cgh[l] = cgh[entityTypeIndex];
            arg0.cgg[l] = cgg[entityTypeIndex];
        }

        public void UpdateShading(bool setShadeValue, int arg1, int arg2, int x, int y, int z)
        {
            clf = 256 - arg1 * 4;
            cle = (64 - arg2) * 16 + 128;
            if (dontRecieveShadows)
                return;
            for (int j = 0; j < face_count; j++)
                if (setShadeValue)
                    gouraud_shade[j] = shadeValue;
                else
                    gouraud_shade[j] = 0;

            cla = x;
            clb = y;
            clc = z;
            // Calculate magnitude (length) of input vector
            cld = (int)Math.Sqrt(x * x + y * y + z * z);
            normalize();
        }

        public void cmf(int j, int k, int x, int y, int z)
        {
            clf = 256 - j * 4;
            cle = (64 - k) * 16 + 128;
            if (dontRecieveShadows)
            {
                return;
            }
            else
            {
                cla = x;
                clb = y;
                clc = z;
                cld = (int)Math.Sqrt(x * x + y * y + z * z);
                normalize();
                return;
            }
        }

        public void cmg(int x, int y, int z)
        {
            if (dontRecieveShadows)
            {
                return;
            }
            else
            {
                cla = x;
                clb = y;
                clc = z;
                // normalized value?
                cld = (int)Math.Sqrt(x * x + y * y + z * z);
                normalize();
                return;
            }
        }

        public void SetVertexColor(int vertIndex, int value)
        {
            vertexColor[vertIndex] = value;
        }

        public void offsetMiniPosition(int x, int y, int z)
        {
            rotationX = rotationX + x & 0xff;
            rotationY = rotationY + y & 0xff;
            rotationZ = rotationZ + z & 0xff;
            cmm();
            objectState = 1;
        }

        public void setRotation(int x, int y, int z)
        {
            rotationX = x & 0xff;
            rotationY = y & 0xff;
            rotationZ = z & 0xff;
            cmm();
            objectState = 1;
        }

        public void offsetPosition(int xOffset, int yOffset, int zOffset)
        {
            positionX += xOffset;
            positionY += yOffset;
            positionZ += zOffset;
            cmm();
            objectState = 1;
        }

        public void setPosition(int x, int y, int z)
        {
            positionX = x;
            positionY = y;
            positionZ = z;
            cmm();
            objectState = 1;
        }

        private void cmm()
        {
            if (ckg != 256 || ckh != 256 || cki != 256 || ckj != 256 || ckk != 256 || ckl != 256)
            {
                ckm = 4;
                return;
            }
            if (ckd != 256 || cke != 256 || ckf != 256)
            {
                ckm = 3;
                return;
            }
            if (rotationX != 0 || rotationY != 0 || rotationZ != 0)
            {
                ckm = 2;
                return;
            }
            if (positionX != 0 || positionY != 0 || positionZ != 0)
            {
                ckm = 1;
                return;
            }
            else
            {
                ckm = 0;
                return;
            }
        }

        private void OffsetWorldVertices(int x, int y, int z)
        {
            for (int j = 0; j < vert_count; j++)
            {
                worldVertX[j] += x;
                worldVertY[j] += y;
                worldVertZ[j] += z;
            }

        }

        private void rotate(int x, int y, int z)
        {
            for (int k2 = 0; k2 < vert_count; k2++)
            {
                if (z != 0)
                {
                    int j = cie[z];
                    int i1 = cie[z + 256];
                    int l1 = worldVertY[k2] * j + worldVertX[k2] * i1 >> 15;
                    worldVertY[k2] = worldVertY[k2] * i1 - worldVertX[k2] * j >> 15;
                    worldVertX[k2] = l1;
                }
                if (x != 0)
                {
                    int k = cie[x];
                    int j1 = cie[x + 256];
                    int i2 = worldVertY[k2] * j1 - worldVertZ[k2] * k >> 15;
                    worldVertZ[k2] = worldVertY[k2] * k + worldVertZ[k2] * j1 >> 15;
                    worldVertY[k2] = i2;
                }
                if (y != 0)
                {
                    int l = cie[y];
                    int k1 = cie[y + 256];
                    int j2 = worldVertZ[k2] * l + worldVertX[k2] * k1 >> 15;
                    worldVertZ[k2] = worldVertZ[k2] * k1 - worldVertX[k2] * l >> 15;
                    worldVertX[k2] = j2;
                }
            }

        }

        private void scaleVertices(int x, int z, int x1, int y, int z1, int y1)
        {
            for (int j = 0; j < vert_count; j++)
            {
                if (x != 0)
                    worldVertX[j] += worldVertY[j] * x >> 8;
                if (z != 0)
                    worldVertZ[j] += worldVertY[j] * z >> 8;
                if (x1 != 0)
                    worldVertX[j] += worldVertZ[j] * x1 >> 8;
                if (y != 0)
                    worldVertY[j] += worldVertZ[j] * y >> 8;
                if (z1 != 0)
                    worldVertZ[j] += worldVertX[j] * z1 >> 8;
                if (y1 != 0)
                    worldVertY[j] += worldVertX[j] * y1 >> 8;
            }

        }

        private void scaleVertices(int x, int y, int z)
        {
            for (int j = 0; j < vert_count; j++)
            {
                worldVertX[j] = worldVertX[j] * x >> 8;
                worldVertY[j] = worldVertY[j] * y >> 8;
                worldVertZ[j] = worldVertZ[j] * z >> 8;
            }

        }

        private void calculateObjectBounds()
        {
            boundsMinX = boundsMinY = boundsMinZ = 0xf423f;
            distVar = (int)(boundsMaxX = boundsMaxY = boundsMaxZ = -boundsMinX/*unchecked((int)0xfff0bdc1)*/);
            for (int j = 0; j < face_count; j++)
            {
                int[] ai = face_vertices[j];
                int l = ai[0];
                int j1 = face_vertices_count[j];
                int minX;
                int maxX = minX = worldVertX[l];
                int minY;
                int maxY = minY = worldVertY[l];
                int minZ;
                int maxZ = minZ = worldVertZ[l];
                for (int k = 0; k < j1; k++)
                {
                    int i1 = ai[k];
                    if (worldVertX[i1] < minX)
                        minX = worldVertX[i1];
                    else
                        if (worldVertX[i1] > maxX)
                            maxX = worldVertX[i1];
                    if (worldVertY[i1] < minY)
                        minY = worldVertY[i1];
                    else
                        if (worldVertY[i1] > maxY)
                            maxY = worldVertY[i1];
                    if (worldVertZ[i1] < minZ)
                        minZ = worldVertZ[i1];
                    else
                        if (worldVertZ[i1] > maxZ)
                            maxZ = worldVertZ[i1];
                }

                if (!noCollider)
                {
                    faceBoundsMinX[j] = minX;
                    faceBoundsMaxX[j] = maxX;
                    faceBoundsMinY[j] = minY;
                    faceBoundsMaxY[j] = maxY;
                    faceBoundsMinZ[j] = minZ;
                    faceBoundsMaxZ[j] = maxZ;
                }

                if (maxX - minX > distVar)
                    distVar = (maxX - minX);
                if (maxY - minY > distVar)
                    distVar = (maxY - minY);
                if (maxZ - minZ > distVar)
                    distVar = (maxZ - minZ);
                if (minX < boundsMinX)
                    boundsMinX = minX;
                if (maxX > boundsMaxX)
                    boundsMaxX = maxX;
                if (minY < boundsMinY)
                    boundsMinY = minY;
                if (maxY > boundsMaxY)
                    boundsMaxY = maxY;
                if (minZ < boundsMinZ)
                    boundsMinZ = minZ;
                if (maxZ > boundsMaxZ)
                    boundsMaxZ = maxZ;
            }

        }

        public void normalize()
        {
            if (dontRecieveShadows)
                return;
            int j = cle * cld >> 8;
            for (int k = 0; k < face_count; k++)
                if (gouraud_shade[k] != shadeValue)
                    gouraud_shade[k] = (normalX[k] * cla + normalY[k] * clb + normalZ[k] * clc) / j;

            int[] ai = new int[vert_count];
            int[] ai1 = new int[vert_count];
            int[] ai2 = new int[vert_count];
            int[] ai3 = new int[vert_count];
            for (int l = 0; l < vert_count; l++)
            {
                ai[l] = 0;
                ai1[l] = 0;
                ai2[l] = 0;
                ai3[l] = 0;
            }

            for (int i1 = 0; i1 < face_count; i1++)
                if (gouraud_shade[i1] == shadeValue)
                {
                    for (int j1 = 0; j1 < face_vertices_count[i1]; j1++)
                    {
                        int l1 = face_vertices[i1][j1];
                        ai[l1] += normalX[i1];
                        ai1[l1] += normalY[i1];
                        ai2[l1] += normalZ[i1];
                        ai3[l1]++;
                    }

                }

            for (int k1 = 0; k1 < vert_count; k1++)
                if (ai3[k1] > 0)
                    cfn[k1] = (ai[k1] * cla + ai1[k1] * clb + ai2[k1] * clc) / (j * ai3[k1]);

        }

        public void calculateNormals()
        {
            if (dontRecieveShadows && noCollider)
                return;
            for (int j = 0; j < face_count; j++)
            {
                int[] ai = face_vertices[j];
                int k = worldVertX[ai[0]];
                int l = worldVertY[ai[0]];
                int i1 = worldVertZ[ai[0]];
                int j1 = worldVertX[ai[1]] - k;
                int k1 = worldVertY[ai[1]] - l;
                int l1 = worldVertZ[ai[1]] - i1;
                int i2 = worldVertX[ai[2]] - k;
                int j2 = worldVertY[ai[2]] - l;
                int k2 = worldVertZ[ai[2]] - i1;

                int xDistance = k1 * k2 - j2 * l1;
                int yDistance = l1 * i2 - k2 * j1;
                int j3;
                for (j3 = j1 * j2 - i2 * k1; xDistance > 8192 || yDistance > 8192 || j3 > 8192 || xDistance < -8192 || yDistance < -8192 || j3 < -8192; j3 >>= 1)
                {
                    xDistance >>= 1;
                    yDistance >>= 1;
                }

                // normalize
                int k3 = (int)(256D * Math.Sqrt(xDistance * xDistance + yDistance * yDistance + j3 * j3));
                if (k3 <= 0)
                    k3 = 1;
                normalX[j] = (xDistance * 0x10000) / k3;
                normalY[j] = (yDistance * 0x10000) / k3;
                normalZ[j] = (j3 * 65535) / k3;
                cgh[j] = -1;
            }

            normalize();
        }

        public void UpdateWorldTransformation()
        {
            if (objectState == 2)
            {
                objectState = 0;
                for (int j = 0; j < vert_count; j++)
                {
                    worldVertX[j] = vert_x[j];
                    worldVertY[j] = vert_y[j];
                    worldVertZ[j] = vert_z[j];
                }


                distVar = (int)(boundsMaxX = boundsMaxY = boundsMaxZ = 0x98967f);
                boundsMinX = (int)(boundsMinY = boundsMinZ = -boundsMaxZ/*unchecked((int)0xff676981)*/);
                return;
            }
            if (objectState == 1)
            {
                objectState = 0;
                for (int k = 0; k < vert_count; k++)
                {
                    worldVertX[k] = vert_x[k];
                    worldVertY[k] = vert_y[k];
                    worldVertZ[k] = vert_z[k];
                }

                if (ckm >= 2)
                    rotate(rotationX, rotationY, rotationZ);
                if (ckm >= 3)
                    scaleVertices(ckd, cke, ckf);
                if (ckm >= 4)
                    scaleVertices(ckg, ckh, cki, ckj, ckk, ckl);
                if (ckm >= 1)
                    OffsetWorldVertices(positionX, positionY, positionZ);
                calculateObjectBounds();
                calculateNormals();
            }
        }

        public void cnh(int arg0, int arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7)
        {
            UpdateWorldTransformation();
            if (boundsMinZ > Camera.farZ || boundsMaxZ < Camera.nearZ || boundsMinX > Camera.farX || boundsMaxX < Camera.nearX || boundsMinY > Camera.farY || boundsMaxY < Camera.nearY)
            {
                visible = false;
                return;
            }
            visible = true;
            int i1 = 0;
            int j1 = 0;
            int k1 = 0;
            int l1 = 0;
            int i2 = 0;
            int j2 = 0;
            if (arg5 != 0)
            {
                i1 = cif[arg5];
                j1 = cif[arg5 + 1024];
            }
            if (arg4 != 0)
            {
                i2 = cif[arg4];
                j2 = cif[arg4 + 1024];
            }
            if (arg3 != 0)
            {
                k1 = cif[arg3];
                l1 = cif[arg3 + 1024];
            }
            for (int k2 = 0; k2 < vert_count; k2++)
            {
                int l2 = worldVertX[k2] - arg0;
                int i3 = worldVertY[k2] - arg1;
                int j3 = worldVertZ[k2] - arg2;
                if (arg5 != 0)
                {
                    int j = i3 * i1 + l2 * j1 >> 15;
                    i3 = i3 * j1 - l2 * i1 >> 15;
                    l2 = j;
                }
                if (arg4 != 0)
                {
                    int k = j3 * i2 + l2 * j2 >> 15;
                    j3 = j3 * j2 - l2 * i2 >> 15;
                    l2 = k;
                }
                if (arg3 != 0)
                {
                    int l = i3 * l1 - j3 * k1 >> 15;
                    j3 = i3 * k1 + j3 * l1 >> 15;
                    i3 = l;
                }
                if (j3 >= arg7)
                    cfl[k2] = (l2 << arg6) / j3;
                else
                    cfl[k2] = l2 << arg6;
                if (j3 >= arg7)
                    cfm[k2] = (i3 << arg6) / j3;
                else
                    cfm[k2] = i3 << arg6;
                cfi[k2] = l2;
                cfj[k2] = i3;
                cfk[k2] = j3;
            }

        }

        public void cni()
        {
            UpdateWorldTransformation();
            for (int j = 0; j < vert_count; j++)
            {
                vert_x[j] = worldVertX[j];
                vert_y[j] = worldVertY[j];
                vert_z[j] = worldVertZ[j];
            }

            positionX = positionY = positionZ = 0;
            rotationX = rotationY = rotationZ = 0;
            ckd = cke = ckf = 256;
            ckg = ckh = cki = ckj = ckk = ckl = 256;
            ckm = 0;
        }

        public GameObject CreateParent()
        {
            GameObject[] ai = new GameObject[1];
            ai[0] = this;
            GameObject j = new GameObject(ai, 1);
            j.cgm = cgm;
            j.isGiantCrystal = isGiantCrystal;
            return j;
        }

        public GameObject CreateParent(bool flag, bool flag1, bool flag2, bool flag3)
        {
            GameObject[] ai = new GameObject[1];
            ai[0] = this;
            GameObject j = new GameObject(ai, 1, flag, flag1, flag2, flag3);
            j.cgm = cgm;
            return j;
        }

        public void CopyTranslation(GameObject j)
        {
            rotationX = j.rotationX;
            rotationY = j.rotationY;
            rotationZ = j.rotationZ;
            positionX = j.positionX;
            positionY = j.positionY;
            positionZ = j.positionZ;
            cmm();
            objectState = 1;
        }

        //public int getShadeValue(sbyte[] _vert_count)
        //{
        //    for (; _vert_count[clg] == 10 || _vert_count[clg] == 13; clg++) ;
        //    int x = cih[_vert_count[clg++]];
        //    int y = cih[_vert_count[clg++]];
        //    int z = cih[_vert_count[clg++]];
        //    int y = (x * 4096 + y * 64 + z) - 0x20000;
        //    if (y == 0x1e240)
        //        y = shadeValue;
        //    return y;
        //}

        public int getShadeValue(sbyte[] arg0)
        {
            for (; arg0[clg] == 10 || arg0[clg] == 13; clg++) ;
            int j = cih[arg0[clg++] & 0xff];
            int k = cih[arg0[clg++] & 0xff];
            int l = cih[arg0[clg++] & 0xff];
            int i1 = (j * 4096 + k * 64 + l) - 0x20000;
            if (i1 == 0x1e240)
                i1 = shadeValue;
            return i1;
        }

        public int vert_count;
        public int[] cfi;
        public int[] cfj;
        public int[] cfk;
        public int[] cfl;
        public int[] cfm;
        public int[] cfn;
        public int[] vertexColor;
        public int face_count;
        public int[] face_vertices_count;
        public int[][] face_vertices;
        public int[] texture_back;
        public int[] texture_front;
        public int[] cgg;
        public int[] cgh;
        public int[] gouraud_shade;
        public int[] normalX;
        public int[] normalY;
        public int[] normalZ;
        public int cgm;
        public int objectState;
        public bool visible;
        public int boundsMinX;
        public int boundsMaxX;
        public int boundsMinY;
        public int boundsMaxY;
        public int boundsMinZ;
        public int boundsMaxZ;
        public bool chh;
        public bool chi;
        public bool isGiantCrystal;
        public int index;
        public int[] entityType;
        public int[] chm;
        private bool chn;
        public bool noCollider;
        public bool dontRecieveShadows;
        public bool cic;
        public bool cid;
        private static int[] cie;
        private static int[] cif;
        private static int[] cig;
        private static int[] cih;
        private int shadeValue;
        public int totalVerticeCount;
        public int[] vert_x;
        public int[] vert_y;
        public int[] vert_z;

        public Vector3[] _vertices;

        public int[] worldVertX;
        public int[] worldVertY;
        public int[] worldVertZ;
        private int totalFaceCount;
        private int[][] cje;
        private int[] faceBoundsMinX;
        private int[] faceBoundsMaxX;
        private int[] faceBoundsMinY;
        private int[] faceBoundsMaxY;
        private int[] faceBoundsMinZ;
        private int[] faceBoundsMaxZ;
        private int positionX;
        private int positionY;
        private int positionZ;
        private int rotationX;
        private int rotationY;
        private int rotationZ;
        private int ckd;
        private int cke;
        private int ckf;
        private int ckg;
        private int ckh;
        private int cki;
        private int ckj;
        private int ckk;
        private int ckl;
        private int ckm;
        private int distVar;
        private int cla;
        private int clb;
        private int clc;
        private int cld;
        public int cle;
        public int clf;
        private int clg;

        static GameObject()
        {
            cie = new int[512];
            cif = new int[2048];
            cig = new int[64];
            cih = new int[256];
            for (int j = 0; j < 256; j++)
            {
                cie[j] = (int)(Math.Sin((double)j * 0.02454369D) * 32768D);
                cie[j + 256] = (int)(Math.Cos((double)j * 0.02454369D) * 32768D);
            }

            for (int k = 0; k < 1024; k++)
            {
                cif[k] = (int)(Math.Sin((double)k * 0.00613592315D) * 32768D);
                cif[k + 1024] = (int)(Math.Cos((double)k * 0.00613592315D) * 32768D);
            }

            for (int l = 0; l < 10; l++)
                cig[l] = (byte)(48 + l);

            for (int i1 = 0; i1 < 26; i1++)
                cig[i1 + 10] = (byte)(65 + i1);

            for (int j1 = 0; j1 < 26; j1++)
                cig[j1 + 36] = (byte)(97 + j1);

            cig[62] = -93;
            cig[63] = 36;
            for (int k1 = 0; k1 < 10; k1++)
                cih[48 + k1] = k1;

            for (int l1 = 0; l1 < 26; l1++)
                cih[65 + l1] = l1 + 10;

            for (int i2 = 0; i2 < 26; i2++)
                cih[97 + i2] = i2 + 36;

            cih[163] = 62;
            cih[36] = 63;
        }
    }
}
